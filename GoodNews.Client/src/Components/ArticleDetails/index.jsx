import React, {useEffect, useState} from 'react';
import {matchPath} from "react-router";
import Container from "@material-ui/core/Container";
import {API_BASE_URL} from "../../config";
import Login from "../Login";
import ArticleContent from "./ArticleContent";
import ArticleComments from "./ArticleComments";

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
            const res = await fetch(`${API_BASE_URL}/News/GetArticle/${articleId}`);
            res
                .json()
                .then(res => setArticle(res))
                .catch(err => setErrors(err));
        }

        fetchData();
    }, []);


    return (
        <Container>
        <ArticleContent article={article}/>
        <ArticleComments artticleId={articleId}/>
        </Container>
    )
}
export default ArticleDetails;
