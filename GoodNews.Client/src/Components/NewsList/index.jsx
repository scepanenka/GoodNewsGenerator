import React, {useEffect, useState} from "react";
import ArticleCard from "../News/ArticleCard";
import Grid from "@material-ui/core/Grid";
import {Container} from "@material-ui/core";
import s from './style.module.scss'
import {API_BASE_URL} from "../../config";
import axios from "axios";
import InfiniteScroll from "react-infinite-scroll-component";


const NewsList = () => {

    const [news, setNews] = useState([]);
    const [page, setPage] = useState(1);

    useEffect(() => {
            axios.get(`${API_BASE_URL}/News/GetNews?page=${page}`)
                .then(res => {setNews(res.data)});
    }, []);

    const fetchData = async () => {
        setPage(page + 1);
        await axios
            .get(`${API_BASE_URL}/News/GetNews?page=${page + 1}`)
            .then(res => setNews(news.concat(res.data)));
    };

    return (
        <Container maxWidth="lg" className={s.newsContainer}>
            <InfiniteScroll dataLength={news.length} next={fetchData} hasMore={true}>
            <Grid container justify="center" spacing={5} overflow="hidden" className={s.newsContainer}>
                {news.map(article =>
                    <ArticleCard key={article.id} article={article}/>
                )}
            </Grid>
            </InfiniteScroll>
        </Container>
    );
};
export default NewsList;
