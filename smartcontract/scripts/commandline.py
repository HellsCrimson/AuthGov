from asyncio.windows_events import NULL
from scripts.app_functions import *


testaccountprivatekey = (
    "0xc41e8481f4b4351d8cb5591411dd7a9860bd410ad3a926cdd11061bb3dbbbde6"
)
testaccountpublickey = "0x5C48120FBc009D31c39a8A6e15D1F112c255E856"


def main():
    private_key = ""
    account = NULL
    published_contract = False
    while True:
        command = input().split()
        change = False
        if command[0] == "quit":
            break

        if command[0] == "--help":
            print(
                "- quit => close command line interface\n"
                + "- publish_contract => publish your new contract\n"
                + "- set_private_address *address* => set account for publishing contract\n"
                + "- add_document *filepath* *name* => save the document at *filepath* with *name in the contract\n"
                + "- add_rec_document *category* *filepath* => save the document at *filepath* in *category* category\n"
                + "- get_document *name* => save the document at current repository as yourdocument.png\n"
                + "- get_rec_document *category* *date* => save the rec document at current repository as yourdocument.png\n"
                + "- get_documents_store => shows the documents stored in the contract\n"
                + "- get_rec_doc_categories => shows all the categories of documents\n"
                + "- get_dates_for_doc_category *category* => shows all the submit depending of the date of a specific category\n"
            )
            change = True

        if command[0] == "publish_contract":
            if account == NULL:
                print("You have to set an account before publishing your contract!")
            else:
                publish_contract(account, private_key)
                published_contract = True
            change = True

        if command[0] == "set_private_address":
            if len(command) == 1:
                account = accounts.add(testaccountprivatekey)
                private_key = testaccountprivatekey
            else:
                account = accounts.add(command[1])
                private_key = command[1]
            change = True

        if command[0] == "add_document":
            if account == NULL or published_contract == False:
                print(
                    "You have to set an account or publish your contract before interacting with it!"
                )
            else:
                addDocument(account, command[1], command[2])
            change = True

        if command[0] == "add_rec_document":
            if account == NULL or published_contract == False:
                print(
                    "You have to set an account or publish your contract before interacting with it!"
                )
            else:
                addRecDocument(account, command[1], command[2])
            change = True

        if command[0] == "get_document":
            if account == NULL or published_contract == False:
                print(
                    "You have to set an account or publish your contract before interacting with it!"
                )
            else:
                getDocument(account, command[1])
            change = True

        if command[0] == "get_rec_document":
            if account == NULL or published_contract == False:
                print(
                    "You have to set an account or publish your contract before interacting with it!"
                )
            else:
                getRecDocument(account, command[1], command[2])
            change = True

        if command[0] == "get_documents_store":
            print(getDocumentsNames(account))
            change = True

        if command[0] == "get_rec_doc_categories":
            print(getCategories(account))
            change = True

        if command[0] == "get_dates_for_doc_category":
            print(getDates(account, command[1]))
            change = True

        if not change:
            print("Type --help for commands")
