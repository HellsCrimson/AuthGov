import pytest
from scripts.deploy_myperson import deploy_my_person
from scripts.helpful_scripts import (
    FORKED_LOCAL_ENVIRONMENTS,
    LOCAL_BLOCKCHAIN_ENVIRONMENTS,
    get_account,
)
from brownie import network


def test_work_on_testnet():
    if (
        network.show_active() in LOCAL_BLOCKCHAIN_ENVIRONMENTS
        or network.show_active() in FORKED_LOCAL_ENVIRONMENTS
    ):
        pytest.skip()
    account = get_account()
    myperson = deploy_my_person()

    doc1 = "abc"
    doc2 = "def"
    doc3 = "ghi"
    doc4 = "jkl"

    doc5 = "mno"
    doc6 = "pqr"
    doc7 = "stu"
    doc8 = "vwx"

    print(myperson.listDocuments({"from": account}))
    myperson.setDocument(doc1, "doc1", {"from": account})

    print(myperson.listDocuments({"from": account}))
    print(myperson.getDocument("doc1", {"from": account}))

    myperson.setRecDocument(doc5, "impot", "12", {"from": account})

    print(myperson.listRecDocumentsCat({"from": account}))
    print(myperson.listRecDocumentsDates("impot", {"from": account}))

    print(myperson.getRecDocument("impot", "12", {"from": account}))
