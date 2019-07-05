import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './suggestion.css';


class Suggestions extends Component{
    
    render () {
        return (
            <ul className={`suggestion-list-wrapper ${this.props.results.length>0?"display-block":"display-none"}`}>
                {
                    this.props.results.map((r, index) => {
                        return (
                            <li key={r.id}>
                                <Link to={`/news-detail/${r._id}`}>{r.title}</Link>
                            </li>
                        );
                    })
                }
            </ul>
        );
    }
}


export default Suggestions