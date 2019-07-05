import React, { Component } from 'react';
import NewsDetail from '../../components/news/news-detail/news-detail.component';

class NewsDetailLayout extends Component {

    constructor(props) {
        super(props);
        this.state = {
        }
    }

    render () {
        return (
            <div className="newsDetail">
                <NewsDetail props={this.props}></NewsDetail>
            </div>
        )
    }
}

export default NewsDetailLayout;