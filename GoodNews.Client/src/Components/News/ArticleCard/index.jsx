import React from 'react';
import {makeStyles} from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Grid from "@material-ui/core/Grid";
import {NavLink} from "react-router-dom";
import s from './style.module.scss'

const useStyles = makeStyles({
    card: {
        maxWidth: 345,
    },
    title: {
        height: 60,
        overflow: "hidden",
    },
    media: {
        height: 180,
    },
    description: {
        height: 100,
        overflow: "hidden",
    }
});

const ArticleCard = (props) => {
    const classes = useStyles();

    return (
        <Grid item>
            <Card className={classes.card}>
                <NavLink to={`/news/${props.article.id}`}>
                    <CardActionArea>
                        <CardMedia
                            className={classes.media}
                            image={props.article.thumbnailUrl}
                        />
                        <CardContent>
                            <Typography
                                className={classes.title}
                                gutterBottom
                                variant="subtitle2"
                                component="h3">
                                <strong>{props.article.title}</strong>
                            </Typography>
                            <Typography
                                className={classes.description}
                                variant="body2"
                                color="textSecondary"
                                align="justify"
                                component="p">
                                {props.article.description}
                            </Typography>
                        </CardContent>
                        <CardContent className={classes.source}>
                            <Typography >
                                {props.article.source}
                            </Typography>
                        </CardContent>
                    </CardActionArea>
                </NavLink>
                <CardActions>
                    <NavLink to={`/news/${props.article.id}`}>
                        <Button variant="contained"
                                color="primary"
                                size="small">
                            Подробнее
                        </Button>
                    </NavLink>
                </CardActions>
            </Card>

        </Grid>
    )
        ;
}

export default ArticleCard;
