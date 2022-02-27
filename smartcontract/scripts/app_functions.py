from turtle import st
from brownie import accounts, MyPerson
from datetime import date
import base64
import io
from PIL import Image
import fitz

testaccountprivatekey = (
    "0xc41e8481f4b4351d8cb5591411dd7a9860bd410ad3a926cdd11061bb3dbbbde6"
)
testaccountpublickey = "0x5C48120FBc009D31c39a8A6e15D1F112c255E856"


def deploy_my_person(account):
    my_person = MyPerson.deploy(
        {"from": account},
    )
    print("MyPerson contract deployed!")
    return my_person


def publish_contract(account, private_key):
    contract = deploy_my_person(account)
    CONTRACT_ADDRESS = contract.address
    PUBLIC_KEY = account.address
    PRIVATE_KEY = private_key
    f = open("publicKey.txt", "w")
    f.write(PUBLIC_KEY)
    f.close()

    f = open("privateKey.txt", "w")
    f.write(PRIVATE_KEY)
    f.close()

    f = open("contractAddress.txt", "w")
    f.write(CONTRACT_ADDRESS)
    f.close()


def addDocument(account, file, name):
    PDF = whatfile(file)
    if PDF:
        string = pdftoimagetostring(file)
    else:
        string = imagetostring(file)
    MyPerson[-1].setDocument(string, name, {"from": account})


def addRecDocument(account, category, file):
    PDF = whatfile(file)
    if PDF:
        string = pdftoimagetostring(file)
    else:
        string = imagetostring(file)
    today = date.today()
    d = today.strftime("%d/%m/%Y")
    MyPerson[-1].setRecDocument(string, category, date, {"from": account})


def getDocument(account, name):
    string = MyPerson[-1].getDocument(name, {"from": account})
    decode(string)


def getRecDocument(account, name, date):
    string = MyPerson[-1].getRecDocument(name, date, {"from": account})
    decode(string)


def getDocumentsNames(account):
    namesarray = MyPerson[-1].listDocuments({"from": account})
    return namesarray


def getCategories(account):
    categoriesarray = MyPerson[-1].listRecDocumentsCat({"from": account})
    return categoriesarray


def getDates(account, name):
    datesarray = MyPerson[-1].listRecDocumentsDates(name, {"from": account})
    return datesarray


def imagetostring(filename):  # image to strings
    with open(filename, "rb") as image:
        base64_string = base64.b64encode(image.read())


def decode(str):  # string to image
    string = str
    decoded_string = io.BytesIO(base64.b64decode(string))
    img = Image.open(decoded_string)
    img.save("yourdocument.png")


def pdftoimagetostring(filename):  # pdf to image to string
    l = len(filename)
    doc = fitz.open(filename)
    pages = doc.pages()
    for page in pages:
        page = doc.load_page(0)
        page = page.get_pixmap()
        filename = list(filename)
        filename[l - 2] = "n"
        filename[l - 1] = "g"
        filename = "".join(filename)
        page.save(filename)
        with open(filename, "rb") as image:
            base64_string = base64.b64encode(image.read())
            return base64_string


def whatfile(filename):  # determine if an image or pdf
    l = len(filename)
    return filename[l - 1] == "f" and filename[l - 2] == "d" and filename[l - 3] == "p"
