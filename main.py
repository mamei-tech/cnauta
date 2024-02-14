import re
from datetime import datetime
from typing import Union

import requests
from requests import RequestException
import configparser

config = configparser.ConfigParser()

# current dateTime
now = datetime.now()
date_time_str: str = now.strftime("%Y%m%d%H%M%S%f")[:-3]

source_hostname: str = 'secure.etecsa.net'
source_port: str = '8443'
source_url: str = "https://" + source_hostname + ":" + source_port
source_url_query_servlet: str = source_url + "/EtecsaQueryServlet"
source_url_login_servlet: str = source_url + "//LoginServlet"
source_url_logout_servlet: str = source_url + "/LogoutServlet"

headers = {
    "Accept-Encoding": "gzip, deflate, br",
    "Accept-Language": "en-US,en;q=0.5",
    "Host": source_hostname,
    "Origin": source_url,
    "Sec-Fetch-Dest": "document",
    "Sec-Fetch-Mode": "navigate",
    "Sec-Fetch-Site": "same-origin",
    "Sec-Fetch-User": "?1",
    "Sec-GPC": "1",
    "sec-ch-ua-mobile": "?0",
    "sec-ch-ua-platform": "\"Windows\"",
    "Upgrade-Insecure-Requests": "1",
    "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) "
                  "Chrome/109.0.0.0 Safari/537.36",
}

# Data to be written in file
dictionary = {
    "attr_uuid": "",
    "logger_id": "",
}


def begin(user: str, passwd: str) -> dict[str, str]:
    try:
        get_request = requests.get(source_url, headers=headers)
    except requests.exceptions.RequestException as e:
        print(e)
        raise e
    else:
        match_wlan_user_ip = re.search(r'<input type="hidden" name="wlanuserip" id="wlanuserip" value="(.+?)"/>',
                                       get_request.text, re.I | re.S)
        wlan_user_ip = match_wlan_user_ip[1]

        match_csrfhw = re.search(r"<input type='hidden' name='CSRFHW' value='(.+?)' />", get_request.text, re.I | re.S)
        csrfhw = match_csrfhw[1]

        return {"wlanuserip": wlan_user_ip, "wlanmac": "", "firsturl": "notFound.jsp", "ssid": "", "usertype": "",
                "gotopage": "/nauta_etecsa/LoginURL/mobile_login.jsp",
                "successpage": "/nauta_etecsa/OnlineURL/mobile_index.jsp",
                "loggerId": date_time_str, "lang": "es_ES", "username": user, "password": passwd,
                "CSRFHW": csrfhw, }


def query_servlet(data: dict[str, str]):
    try:
        get_request = requests.get(source_url, headers=headers)
        post_request = requests.post(source_url_query_servlet, headers=headers, cookies=get_request.cookies, data=data)
    except requests.exceptions.RequestException as e:
        print(e)
        raise e
    else:
        # Extract the loggerId value using regular expressions
        logger_id = re.search(r'<input type="hidden" name="loggerId" id="loggerId" value="([^"]+)"', post_request.text)

        if logger_id:
            logger_id = logger_id.group(1)
            print(logger_id)
        else:
            logger_id = ""
            print("loggerId not found")
        return logger_id


def do_login(user: str, passwd: str):
    try:
        post_request = requests.post(source_url_login_servlet, data={"username": user, "password": passwd, })
    except requests.exceptions.RequestException as e:
        print(e)
        raise e
    else:
        match_user_login = re.search(r'"El usuario ya está conectado."', post_request.text, re.I | re.S)
        result = 0
        attr_uuid = ""

        if match_user_login:
            print(match_user_login.group())
            result = 1
        else:
            match_attr_uuid = re.search(r"ATTRIBUTE_UUID=(.+?)&", post_request.text, re.I | re.S)
            attr_uuid = match_attr_uuid[1]
            print("ATTRIBUTE_UUID: ", attr_uuid)
        return {"attr_uuid": attr_uuid, "response": result}


def do_logout(user: str, login_strptime: str, attr_uuid: str):
    data_logout = {'username': user, 'ATTRIBUTE_UUID': attr_uuid, 'wlanacname': '', 'domain': '', 'remove': 1,
                   "loggerId": f"{login_strptime}+{user}"}

    print("---------------------begin: LogoutServlet--------------------")
    try:
        post_request = requests.post(source_url_logout_servlet, data=data_logout)
    except requests.exceptions.RequestException as e:
        print(e)
        raise e
    else:
        print(post_request.text)
        print("----------------------end: LogoutServlet-------------------")


def sign_in(user: str, passwd: str):
    """
    To log in with the Nauta portal to use this function
    :param user:
    :param passwd:
    :return:
    """
    data = begin(user, passwd)

    attr_uuid = do_login(user, passwd)
    # If the response key is 0 satisfactory, but it is 1 then the user is already authenticated
    if attr_uuid.get('response', 1) == 0:
        # get loggerId
        logger_id = query_servlet(data)

        config.set('INFO', 'attr_uuid', attr_uuid.get('attr_uuid', ''))
        config.set('INFO', 'logger_id', logger_id)
        config.set('INFO', 'wlanuserip', data.get('wlanuserip', ''))

        # writing the necessary info to the file to use later in the logout
        with open('config.cfg', 'w') as configfile:
            config.write(configfile)
        print("Login")


def sign_out(user: str):
    """
    To logout with the Nauta portal to use this function
    :param passwd:
    :param user:
    :return:
    """
    config.read('config.cfg')
    do_logout(user, config.get('INFO', "logger_id"), config.get('INFO', "attr_uuid"))


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    config.read('config.cfg')
    username = config.get('DEFAULT', "username")
    password = config.get('DEFAULT', "password")

    if username != "" and password != "":
        sign_in(username, password)

    # if username != "":
    #     sign_out(username)
