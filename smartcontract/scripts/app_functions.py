from scripts.deploy_myperson import deploy_my_person
from brownie import accounts, MyPerson

PRIVATE_KEY = ""
PUBLIC_KEY = ""
CONTRACT_ADDRESS = ""

testaccountprivatekey = (
    "c41e8481f4b4351d8cb5591411dd7a9860bd410ad3a926cdd11061bb3dbbbde6"
)
testaccountpublickey = "0x5C48120FBc009D31c39a8A6e15D1F112c255E856"


def deploy_my_person(private_key):
    account = accounts.add(private_key)
    PRIVATE_KEY = private_key
    PUBLIC_KEY = account
    my_person = MyPerson.deploy(
        {"from": account},
    )
    print("MyPerson contract deployed!")
    return my_person


def publish_contract():
    contract = deploy_my_person(
        "c41e8481f4b4351d8cb5591411dd7a9860bd410ad3a926cdd11061bb3dbbbde6"
    )
    CONTRACT_ADDRESS = contract.address

    f = open("../../AuthGov/publicKey.txt")
    f.write(PUBLIC_KEY)
    f.close()

    f = open("../../AuthGov/privateKey.txt")
    f.write(PRIVATE_KEY)
    f.close()

    f = open("../../AuthGov/contractAddress.txt")
    f.write(CONTRACT_ADDRESS)
    f.close()


def addDocument(account, file):
    MyPerson[-1].setDocument()


def addRecDocument(account):
    pass


def getDocument():
    pass


def getRecDocument():
    pass


def getDocumentsNames():
    pass


def getCategories():
    pass


def getDates():
    pass


def main():
    publish_contract()
