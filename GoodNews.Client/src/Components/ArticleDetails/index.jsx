import React, {useEffect, useState} from 'react';
import {matchPath} from "react-router";
import Container from "@material-ui/core/Container";
import Parser from 'html-react-parser';

const ArticleDetails = (props) => {

    const [hasError, setErrors] = useState(false);
    const [article, setArticle] = useState([]);

    const match = matchPath(props.history.location.pathname, {
        path: '/news/:id',
        exact: true,
        strict: false
    });

    let articleId = match.params.id;



    useEffect(() => {
        async function fetchData() {
            const res = await fetch(`https://localhost:44317/api/news/${articleId}`);
            res
                .json()
                .then(res => setArticle(res))
                .catch(err => setErrors(err));
        }
        fetchData();
    }, []);


    // const getContent = () => {__html: 'First &middot; Second'};

    return (
        <Container align="justify">
            <h1>{article.title}</h1>
            {require('html-react-parser')(
                `${article.content}`
            )}
        </Container>
    )
}
 export default ArticleDetails;
