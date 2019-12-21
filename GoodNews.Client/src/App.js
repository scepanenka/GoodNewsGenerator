import React from 'react';
import {BrowserRouter, Route} from "react-router-dom";
import './App.css';
import Login from "./Components/Login";
import Register from "./Components/Register";
import Header from "./Components/Header";
import News from "./Components/News";
import ArticleDetails from "./Components/ArticleDetails";
import {UserProvider} from "./hooks/useUser";
//import NewsList from "./Components/NewsList";

const App = () => {
    return (
        <BrowserRouter>
            <div className="App">
                <UserProvider>
                    <Header/>
                    <div className='app-content-wrapper'>
                        <Route exact path='/' component={News}/>
                        <Route exact path='/news' component={News}/>
                        <Route path='/news/:id' component={ArticleDetails}/>
                        <Route path='/login' component={Login}/>
                        <Route path='/register' component={Register}/>
                    </div>
                </UserProvider>
            </div>
        </BrowserRouter>
    );
}
export default App;
