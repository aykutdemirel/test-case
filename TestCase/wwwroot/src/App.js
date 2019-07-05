import React, { Component } from 'react';
import {connect} from 'react-redux';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import './App.css';
import Header from './components/header/header.component';
import MainPage from './layouts/client/main-page.layout';
import NewsDetailLayout from './layouts/client/news-detail.layout';
import AddNewsLayout from './layouts/admin/news/add-news.layout';


class App extends Component {
  
  state = {
    todos:[]
  }


  render(){
    return (
      <BrowserRouter>
        <div className="App">
          <Header></Header>
          <div className="container">
            <div className="jumbotron">
                <Switch>
                  <Route exact path="/" component={MainPage} />
                  <Route path="/news-detail/:id" component={NewsDetailLayout} />
                  <Route path="/admin/add-news" component={AddNewsLayout} />
                </Switch>
            </div>
          </div>
        </div>
      </BrowserRouter>
    );
  }
}

const mapStateToProps = state => { 
  return { todos: state.todos }; 
};

export default connect(mapStateToProps)(App);
