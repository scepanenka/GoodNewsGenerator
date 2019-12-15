import React, {useEffect, useState} from "react";
import ArticlePreview from "./ArticlePreview";
import Container from '@material-ui/core/Container';

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
        <Container>
            {news.map(article =>
                <ArticlePreview key={article.id} article={article}/>
            )}
        </Container>
    );
};
export default News;
