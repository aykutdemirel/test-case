import React, { Component } from 'react';
import axios from 'axios';
import { requestHeaders } from './../../const/headers.const';
import { serverPath } from './../../const/server-path.const';
import Suggestions from '../suggestion/suggestion.component';

import './searchbox.css';

class SearchBox extends Component {

    constructor(props) {
        super(props);
        this.state = {
            query: '',
            results: []
        }
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    getInfo = () => {
        axios.get(`${serverPath}/news/search?query=${this.state.query}&page=1&pageSize=10`,requestHeaders)
        .then(response => {
            this.setState({
                results: response.data
            })
        }).catch(error => {
            console.log('axios error | ',error)
        });
    }    

    handleInputChange = () => {
        this.setState({
            query: this.search.value
        }, () => {
            if (this.state.query && this.state.query.length > 2) {
                this.getInfo()
            } else if (!this.state.query) {
                this.setState({
                    results: []
                })
            }
        })
    }  

    render () {
        return (
            <form className="main-search-box form-inline my-2 my-lg-0">
                <input className="form-control form-control-sm mr-sm-2" type="search" ref={input => this.search = input} onChange={this.handleInputChange} placeholder="Ara" aria-label="Ara" />
                <Suggestions results={this.state.results} />
            </form>
        );
    }
}

export default SearchBox;