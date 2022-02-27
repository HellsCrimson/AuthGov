from brownie import accounts, network


def get_account(private_key):
    return accounts.add(private_key)
