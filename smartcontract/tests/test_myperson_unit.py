import pytest
from scripts.deploy_myperson import deploy_my_person
from scripts.helpful_scripts import (
    FORKED_LOCAL_ENVIRONMENTS,
    LOCAL_BLOCKCHAIN_ENVIRONMENTS,
    get_account,
)
from brownie import network, exceptions


def test_add_unique_document():
    if (
        network.show_active() not in LOCAL_BLOCKCHAIN_ENVIRONMENTS
        or network.show_active() not in FORKED_LOCAL_ENVIRONMENTS
    ):
        pytest.skip
    myperson = deploy_my_person()
    print(myperson)
    docunique = "abcdefhij"
    docunique2 = "abcdefhij"
    docunique3 = "abcdefhij"
    docunique4 = "abcdefhij"
    myperson.setDocument(docunique, "doc1")
    myperson.setDocument(docunique2, "doc2")
    myperson.setDocument(docunique3, "doc3")
    myperson.setDocument(docunique4, "doc4")
    print(myperson.listDocuments())
    assert myperson.getDocument("doc1") == docunique
    assert myperson.getDocument("doc2") == docunique
    assert myperson.getDocument("doc3") == docunique
    assert myperson.getDocument("doc4") == docunique


def test_only_owner():
    if (
        network.show_active() not in LOCAL_BLOCKCHAIN_ENVIRONMENTS
        or network.show_active() not in FORKED_LOCAL_ENVIRONMENTS
    ):
        pytest.skip
    account = get_account()
    myperson = deploy_my_person()
    docunique = "abcdefhij"
    myperson.setDocument(docunique, "doc1", {"from": account})
    with pytest.raises(exceptions.VirtualMachineError):
        myperson.getDocument("doc1", {"from": get_account(index=1)})


def test_add_rec_document():
    if (
        network.show_active() not in LOCAL_BLOCKCHAIN_ENVIRONMENTS
        or network.show_active() not in FORKED_LOCAL_ENVIRONMENTS
    ):
        pytest.skip
    account = get_account()
    myperson = deploy_my_person()
    doc = "abcdefhij"
    doc2 = "abcdefhij"
    doc3 = "abcdefhij"
    doc4 = "abcdefhij"
    cat1 = "impot"
    cat2 = "eau"
    date1 = "26/02/22"
    date2 = "27/02/22"
    myperson.setRecDocument(doc, cat1, date1)
    myperson.setRecDocument(doc2, cat1, date1)
    myperson.setRecDocument(doc3, cat2, date2)
    myperson.setRecDocument(doc4, cat2, date2)
    print(myperson.listRecDocumentsCat())
    print(myperson.listRecDocumentsDates(cat1))
    print(myperson.listRecDocumentsDates(cat2))
    assert myperson.getRecDocument(cat1, date1) == doc
    assert myperson.getRecDocument(cat1, date1) == doc
    assert myperson.getRecDocument(cat2, date2) == doc
    assert myperson.getRecDocument(cat2, date2) == doc
