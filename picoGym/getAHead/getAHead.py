import requests

s = requests.Session()

url = 'http://mercury.picoctf.net:15931/index.php'

resp = s.head(url)

print(resp.headers)

