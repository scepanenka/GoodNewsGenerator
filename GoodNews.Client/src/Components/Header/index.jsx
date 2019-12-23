import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import s from './style.module.scss'
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import {NavLink} from "react-router-dom";
import { useUser} from "../../hooks/useUser";
import Typography from "@material-ui/core/Typography";
import Box from "@material-ui/core/Box";

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
        <Box>
            <AppBar position="static">
                <Toolbar className={s.wrapper}>
                    <div className={s.left}>
                        <IconButton edge="start" className={s.menuButton} color="inherit" aria-label="menu">
                            <MenuIcon/>
                        </IconButton>
                        <NavLink to='/news'><Button color="inherit"
                        ><img className={s.logo} src="goodNewsWhite.png" alt="logo"/> </Button></NavLink>
                    </div>
                    <Box component="div" display="inline" mr={3}>
                        <Typography  variant="subtitle2" color="inherit" display="inline" >
                            {user.email ? user.email : 'Please, log in'}
                        </Typography>
                        {user.email
                            ? <Button color="inherit"  onClick={logout} ml={2}>Log Out</Button>
                            :
                            <Box display="inline" ml={2}>
                                <NavLink to='/login'><Button color="inherit">Login</Button></NavLink>
                                <NavLink to='/register'><Button color="inherit">Register</Button></NavLink>
                            </Box>
                        }
                    </Box>
                </Toolbar>
            </AppBar>
        </Box>
    )
}
export default Header;
