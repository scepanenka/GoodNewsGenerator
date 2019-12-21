import parse from "html-react-parser";
import Container from "@material-ui/core/Container";
import React from "react";

const ArticleContent = (props) => {
    return (
        <Container align="justify">
            <h1>{props.article.title}</h1>
            {parse(`${props.article.content}`)}
        </Container>
    )
}

export default ArticleContent
