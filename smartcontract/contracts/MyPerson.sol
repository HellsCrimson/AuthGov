// SPDX-License-Identifier: MIT

pragma solidity ^0.8.0; //solidity version

import "@openzeppelin/contracts/access/Ownable.sol";

contract MyPerson is Ownable {
    string[] private uniquedocuments; //[compileof(id), compileof(passport), compileof(electoralmap), etc...]
    string[] private namedocuments; //[id, passport, electoralmap, etc...]

    string[][] private recursivedocuments; //[ [compileof(bills1), compileof(bills2)] , [] , etc..]
    string[] private categoriesdocuments; //[gas bills, taxes, water bills]
    string[][] private datingrecdocs; //[ [11/05/26 , etc..] , etc...]

    string[] private newdocs;
    string[] private newdate;

    function compareStrings(string memory a, string memory b)
        private
        view
        returns (bool)
    {
        return (keccak256(abi.encodePacked((a))) ==
            keccak256(abi.encodePacked((b))));
    }

    function setDocument(
        //add a new unique doc
        string memory compileddoc,
        string memory namedoc
    ) public onlyOwner {
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
        ///add a new rec doc to its catecory with date
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
            newdocs = new string[](0);
            newdocs.push(compileddoc); //creates a new string array with the new document
            recursivedocuments.push(newdocs); //add this new string array to the main array

            newdate = new string[](0);
            newdate.push(date);
            datingrecdocs.push(newdate);

            categoriesdocuments.push(catdoc);
        } else {
            // if categories already exist
            recursivedocuments[uint256(ind)].push(compileddoc);
            datingrecdocs[uint256(ind)].push(date);
        }
    }

    function getDocument(
        //get a specific file from it name
        string memory namedoc
    ) public view onlyOwner returns (string memory) {
        for (int256 i = 0; i < int256(namedocuments.length); i++) {
            if (compareStrings(namedocuments[uint256(i)], namedoc)) {
                return uniquedocuments[uint256(i)];
            }
        }
        return "error";
    }

    function listDocuments() public view onlyOwner returns (string[] memory) {
        //get all single documents names
        return namedocuments;
    }

    function listRecDocumentsCat()
        public
        view
        onlyOwner
        returns (string[] memory)
    {
        return categoriesdocuments;
    }

    function listRecDocumentsDates(
        string memory namedoc ///get an array of documents of a type
    ) public view onlyOwner returns (string[] memory) {
        for (int256 i = 0; i < int256(categoriesdocuments.length); i++) {
            if (compareStrings(categoriesdocuments[uint256(i)], namedoc)) {
                return datingrecdocs[uint256(i)];
            }
        }
    }

    function getRecDocuments(string memory namedoc, string memory date)
        public
        view
        onlyOwner
        returns (string memory)
    {
        for (int256 i = 0; i < int256(categoriesdocuments.length); i++) {
            if (compareStrings(categoriesdocuments[uint256(i)], namedoc)) {
                for (
                    int256 j = 0;
                    j < int256(datingrecdocs[uint256(i)].length);
                    j++
                ) {
                    if (
                        compareStrings(
                            datingrecdocs[uint256(i)][uint256(j)],
                            date
                        )
                    ) {
                        return recursivedocuments[uint256(i)][uint256(j)];
                    }
                }
            }
        }
    }
}
