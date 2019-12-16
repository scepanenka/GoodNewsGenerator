import React from 'react';
import {BrowserRouter, Route} from "react-router-dom";
import './App.css';
import Login from "./Components/Login";
import Register from "./Components/Register";
import Header from "./Components/Header";
import News from "./Components/News";
import ArticleDetails from "./Components/News/ArticleDetails";

const App = () => {
    return (
        <BrowserRouter>
            <div className="App">
                <Header/>
                <div className='app-content-wrapper'>
                    <Route path='/News' component={News}/>
                    <Route path='/News/id' component={ArticleDetails}/>
                    <Route path='/login' component={Login}/>
                    <Route path='/register' component={Register}/>
                </div>
            </div>
        </BrowserRouter>
    );
}
export default App;
