import React, {useEffect, useState} from "react";
import ArticleCard from "./ArticleCard";
import Grid from "@material-ui/core/Grid";
import {Container} from "@material-ui/core";
import s from './style.module.scss'
import {API_BASE_URL} from "../../config";
import {UserProvider} from "../../hooks/useUser";


const News = (props) => {

    const [hasError, setErrors] = useState(false);
    const [news, setNews] = useState([]);
    const [page, setPage] = useState(1);

    useEffect(() => {
        async function fetchData() {
            const res = await fetch(`${API_BASE_URL}/News/GetNews`);
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
                    <UserProvider>
                        <ArticleCard key={article.id} article={article}/>
                    </UserProvider>
                )}
            </Grid>
        </Container>
    );
};
export default News;
