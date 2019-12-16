import React, {useEffect, useState} from "react";
import ArticleCard from "./ArticleCard";
import Grid from "@material-ui/core/Grid";
import {Container} from "@material-ui/core";
import s from './style.module.scss'


const News = (props) => {

    const [hasError, setErrors] = useState(false);
    const [news, setNews] = useState([]);

    useEffect(() => {
        async function fetchData() {
            const res = await fetch("https://localhost:44317/api/News");
            res
                .json()
                .then(res => setNews(res))
                .catch(err => setErrors(err));
        }

        fetchData();
    }, []);

    return (
        <Container maxWidth="lg" className={s.newsContainer}>
            <Grid container justify="center" spacing={5} >
                {news.map(article =>
                    <ArticleCard key={article.id} article={article}/>
                )}
            </Grid>
        </Container>
    );
};
export default News;
