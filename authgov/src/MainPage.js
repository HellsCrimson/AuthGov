import logo from './authgovlogo.svg';
import './MainPage.css';
import {useState} from "react";
const initialBoard = {
    columns: [
        {
            id: 1,
            title: "ID Card",
            cards: [
                {
                    id: 1,
                    title: "MayorHouse website",
                    description: "URL"
                }
            ]
        },
        {
            id: 2,
            title: "Passport",
            cards: [
                {
                    id: 2,
                    title: "Passport Website",
                    description: "URL"
                }
            ]
        },
        {
            id:3,
            title: "Visa",
            cards:[
                {
                    id:3,
                    title:"Visa Request",
                    description: "URL"
                }
            ]
        }
    ]
};
function MainPage()
{
    const [board, setBoard] = useState(initialBoard);
    return(
        <div className="AuthGov">
            <header className={"AuthGov"}>
                <div className={"SideBoradLink"}>
                    #addboard
                </div>
                <div className={"Account"}>
                    #set Account
                </div>
                <div className={"Locker"}>
                    #addboardlocker
                </div>
            </header>

        </div>
    );


}