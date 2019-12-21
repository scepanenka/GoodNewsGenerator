import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import s from './style.module.scss'
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import {NavLink} from "react-router-dom";
import {UserProvider, useUser} from "../../hooks/useUser";
import Typography from "@material-ui/core/Typography";

const styles = {
    grow: {
        flexGrow: 1,
    },
    menuButton: {
        marginLeft: -12,
        marginRight: 20,
    },
};

const Header = (props) => {
    const {user, setAccessToken} = useUser();

    function logout() {
        setAccessToken(null);
    }

    return (
        <div>
            <AppBar position="static">
                <Toolbar className={s.wrapper}>
                    <div className={s.left}>
                        <IconButton edge="start" className={s.menuButton} color="inherit" aria-label="menu">
                            <MenuIcon/>
                        </IconButton>
                        <NavLink to='/news'><Button color="inherit">GoodNews</Button></NavLink>
                    </div>
                    <div>
                        <Typography variant="h6" color="inherit">
                            {user.email ? user.email : 'Please, log in'}
                        </Typography>
                        {user.email && <Button color="inherit" onClick={logout}>Log Out</Button>}
                        <NavLink to='/login'><Button color="inherit">Login</Button></NavLink>
                        <NavLink to='/register'><Button color="inherit">Register</Button></NavLink>
                    </div>
                </Toolbar>
            </AppBar>
        </div>
    )
}
export default Header;
