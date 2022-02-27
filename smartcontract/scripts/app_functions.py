from scripts.deploy_myperson import deploy_my_person
from brownie import accounts, MyPerson
from datetime import date
import base64
import io
from PIL import Image
import fitz

PRIVATE_KEY = ""
PUBLIC_KEY = ""
CONTRACT_ADDRESS = ""

testaccountprivatekey = (
    "c41e8481f4b4351d8cb5591411dd7a9860bd410ad3a926cdd11061bb3dbbbde6"
)
testaccountpublickey = "0x5C48120FBc009D31c39a8A6e15D1F112c255E856"


def deploy_my_person(account):
    PUBLIC_KEY = account
    my_person = MyPerson.deploy(
        {"from": account},
    )
    print("MyPerson contract deployed!")
    return my_person


def publish_contract(account):
    contract = deploy_my_person(account)
    CONTRACT_ADDRESS = contract.address

    f = open("../../AuthGov/publicKey.txt", "w")
    f.write(PUBLIC_KEY)
    f.close()

    f = open("../../AuthGov/privateKey.txt", "w")
    f.write(PRIVATE_KEY)
    f.close()

    f = open("../../AuthGov/contractAddress.txt", "w")
    f.write(CONTRACT_ADDRESS)
    f.close()


TYPE = True
PDF = False


def add(account, file):
    if TYPE:
        addDocument(account, file, """category""")
    else:
        addRecDocument(account, """category""", file)


def addDocument(account, file, name):
    whatfile(file)
    if PDF:
        string = pdftoimagetostring(file)
    else:
        string = imagetostring(file)
    MyPerson[-1].setDocument(string, name, {"from": account})


def addRecDocument(account, category, file):
    whatfile(file)
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
    # provide this array to web app to display documents names


def getCategories(account):
    categoriesarray = MyPerson[-1].listRecDocumentsCat({"from": account})
    # provide this array to web app to display categories docs


def getDates(account, name):
    datesarray = MyPerson[-1].listRecDocumentsDates(name, {"from": account})
    # provide this array to web app to display dates of a specific category docs


def whatType():
    # choose on web app if unique doc or multiple ones and set TYPE
    pass


def imagetostring():  # image to string
    with open("class_diagram_19205373.png", "rb") as image:
        base64_string = base64.b64encode(image.read())
        return base64_string


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


def whatfile(filename):  # determine if an image or pdf
    l = len(filename)
    if filename[l - 1] == "f" and filename[l - 2] == "d" and filename[l - 3] == "p":
        PDF = True
    else:
        PDF = False


def main():
    private_key = testaccountprivatekey
    PRIVATE_KEY = private_key
    account = accounts.add(private_key)
    publish_contract(account)
