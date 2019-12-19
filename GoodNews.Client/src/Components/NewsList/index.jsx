import React, {useEffect, useState} from "react";
import ArticleCard from "../News/ArticleCard";
import Grid from "@material-ui/core/Grid";
import {Container} from "@material-ui/core";
import s from './style.module.scss'


const NewsList = () => {

    const [hasError, setErrors] = useState(false);
    const [news, setNews] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [hasMore, setHasMore] = useState(true);

    useEffect(() => {
        async function fetchData() {
            const res = await fetch(`https://good-news-server.azurewebsites.net/api/News?page=${pageNumber}`);
            res
                .json()
                .then(res => {
                    setNews(news.concat(res.data));
                    setPageNumber(pageNumber + 1)
                })
                .catch(err => {
                    console.error(err);
                    setHasMore(false);
                });
            ;
        }

        fetchData();
    }, []);

    return (
        <Container maxWidth="lg" className={s.newsContainer}>
            <Grid container justify="center" spacing={5}>
                {news.map(article =>
                    <ArticleCard key={article.id} article={article}/>
                )}
            </Grid>
        </Container>
    );
};
export default NewsList;
