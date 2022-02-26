// SPDX-License-Identifier: MIT

pragma solidity ^0.8.0; //solidity version

import "@openzeppelin/contracts/access/Ownable.sol";

contract MyPerson is Ownable {
    string[] private uniquedocuments; //[compileof(id), compileof(passport), compileof(electoralmap), etc...]
    string[] private namedocuments; //[id, passport, electoralmap, etc...]

    string[][] private recursivedocuments; //[ [compileof(bills1), compileof(bills2)] , [] , etc..]
    string[] private categoriesdocuments; //[gas bills, taxes, water bills]
    string[][] private datingrecdocs; //[ [11/05/26 , etc..] , etc...]

    function compareStrings(string memory a, string memory b)
        private
        view
        returns (bool)
    {
        return (keccak256(abi.encodePacked((a))) ==
            keccak256(abi.encodePacked((b))));
    }

    function setDocument(string memory compileddoc, string memory namedoc)
        public
        onlyOwner
    {
        int256 ind = -1;
        for (int256 i = 0; i < int256(namedocuments.length); i++) {
            if (compareStrings(namedocuments[uint256(i)], namedoc)) {
                ind = i;
                break;
            }
        }
        if (ind == -1) {
            uniquedocuments.push(compileddoc);
            namedocuments.push(namedoc);
        } else {
            uniquedocuments[uint256(ind)] = compileddoc;
        }
    }

    function setRecDocument(
        string memory compileddoc,
        string memory catdoc,
        string memory date
    ) public onlyOwner {
        int256 ind = -1;
        for (int256 i = 0; i < int256(categoriesdocuments.length); i++) {
            if (compareStrings(categoriesdocuments[uint256(i)], catdoc)) {
                ind = i;
                break;
            }
        }
        if (ind == -1) {
            // if there is no categories
            string[] storage newdocs;
            newdocs.push(compileddoc); //creates a new string array with the new document
            recursivedocuments.push(newdocs); //add this new string array to the main array

            string[] storage newdate;
            //newdate.push(date);
            datingrecdocs.push(newdate);
            datingrecdocs[datingrecdocs.length - 1].push(date);

            categoriesdocuments.push(catdoc);
        } else {
            // if categories already exist
            recursivedocuments[uint256(ind)].push(compileddoc);
            datingrecdocs[uint256(ind)].push(date);
        }
    }

    function getDocument(string memory namedoc)
        public
        view
        onlyOwner
        returns (string memory)
    {
        for (int256 i = 0; i < int256(namedocuments.length); i++) {
            if (compareStrings(namedocuments[uint256(i)], namedoc)) {
                return uniquedocuments[uint256(i)];
            }
        }
        return "error";
    }
}
