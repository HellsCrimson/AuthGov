from brownie import MyPerson, config, network
from scripts.helpful_scripts import get_account


def deploy_my_person():
    account = get_account()
    my_person = MyPerson.deploy(
        {"from": account},
        publish_source=config["networks"][network.show_active()].get("verify", False),
    )
    print("MyPerson contract deployed!")
    return my_person


def main():
    deploy_my_person()
