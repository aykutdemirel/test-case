import React, { Component } from 'react';
import AddNews from '../../../components/news/add-news/add-news.component';

class AddNewsLayout extends Component {

    render () {
        return (
            <div className="addNews">
                <AddNews></AddNews>
            </div>
        )
    }
}

export default AddNewsLayout;