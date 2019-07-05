import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import 'react-notifications/lib/notifications.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import { Provider } from 'react-redux';
import { createStore } from 'redux';
import axios from 'axios';
import newsSearchResultReducer from "./reducers/news-search-result.reducer";

const store = createStore(
    newsSearchResultReducer,
    window.devToolsExtension && window.devToolsExtension()
);

ReactDOM.render(<Provider store={store}><App /></Provider>, document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
