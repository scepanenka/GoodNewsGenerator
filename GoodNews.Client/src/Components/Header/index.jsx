import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import s from './style.module.scss'
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import Typography from "@material-ui/core/Typography";
import {NavLink} from "react-router-dom";

const Header = () => {
    return(
        <div>
            <AppBar position="static">
                <Toolbar className={s.wrapper}>
                    <div className={s.left}>
                        <IconButton edge="start" className={s.menuButton} color="inherit" aria-label="menu">
                            <MenuIcon />
                        </IconButton>
                        <NavLink to='/news'><Button color="inherit">GoodNews</Button></NavLink>
                    </div>
                    <div>
                        <NavLink to='/login'><Button color="inherit">Login</Button></NavLink>
                        <NavLink to='/register'><Button color="inherit">Register</Button></NavLink>

                    </div>
                </Toolbar>
            </AppBar>
        </div>
    )
}
export default Header;
