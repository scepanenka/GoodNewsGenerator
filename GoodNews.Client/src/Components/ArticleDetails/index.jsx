import React, {useEffect, useState} from 'react';
import {matchPath} from "react-router";
import Container from "@material-ui/core/Container";

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

    const getContent = () => {

    }

    return (
        <Container id="articleContent">
            <h1>{article.title}</h1>
            <div>{article.text}</div>
        </Container>
    )
}
 export default ArticleDetails;
