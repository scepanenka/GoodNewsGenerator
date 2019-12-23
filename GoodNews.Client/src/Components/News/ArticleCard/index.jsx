import React from 'react';
import {makeStyles} from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import { Rating } from '@material-ui/lab'
import CardMedia from '@material-ui/core/CardMedia';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Grid from "@material-ui/core/Grid";
import {NavLink} from "react-router-dom";
import s from './style.module.scss'
import StarBorderIcon from '@material-ui/icons/StarBorder';
import Moment from "react-moment";

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
    },
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
                            image={props.article.thumbnailUrl || 'gn3.jpg'}
                            children={
                                    <div className={s.mediaChildTop}>
                                        <span className={s.source}>{props.article.source}</span>
                                        <span>{props.article.category}</span>
                                    </div>
                            }
                        />

                        <div className={s.cardContent}>
                            <Typography
                                className={classes.title}
                                gutterBottom
                                variant="subtitle2"
                                component="h3">
                                <strong>{props.article.title}</strong>
                            </Typography>
                            <div className={s.date}><span>{<Moment format="YYYY/MM/DD HH:mm">{props.article.datePublication}</Moment>}</span></div>
                            <Typography
                                className={classes.description}
                                variant="body2"
                                color="textSecondary"
                                align="justify"
                                component="p">
                                {props.article.description}
                            </Typography>
                        </div>
                    </CardActionArea>
                </NavLink>
                <div className={s.cardBottom}>
                    <NavLink to={`/news/${props.article.id}`}>
                        <Button variant="contained"
                                color="primary"
                                size="small">
                            Подробнее
                        </Button>
                    </NavLink>
                    <div>
                        <Rating name="sentiment"
                                value={props.article.sentimentRating +1.5}
                                readOnly
                                size="medium"
                                precision={0.1}
                                emptyIcon={<StarBorderIcon fontSize="inherit" />}
                        />
                        <span className={s.rating}>{(props.article.sentimentRating +1.5).toFixed(2)}</span>
                    </div>
                </div>
            </Card>

        </Grid>
    )
        ;
}

export default ArticleCard;
