# import requests
# from requests.models import HTTPBasicAuth

# s = requests.Session()

# resp = s.get("http://natas5.natas.labs.overthewire.org", auth=HTTPBasicAuth('natas5','iX6IOfmpN7AYOQGPwtn3fXpbaJVJcHfq'),
# cookies={'loggedin':'1'})
# print(resp.status_code)
# print(resp.text)
import base64

originalString = '111101001111010101000101100011010000110111010001101101010011010110110101101100001100010101011001101001010101100011001101100010'
newString = ''
i = len(originalString) - 1
while(i >= 0):
    newString += originalString[i]
    i -= 1

print(newString)
newString += '00'
password = base64.b64decode(newString)
print(str(password))

