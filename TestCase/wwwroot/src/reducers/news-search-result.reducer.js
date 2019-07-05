import { SEARCH_NEWS, ADD_VISITED_NEWS } from '../actions/news-search-result.action';


export default function newsSearchResultReducer(state = '', {type, payload}) {
    switch (type) {
        case SEARCH_NEWS :
            // thunk ile buradan search edilecek

            return payload;
        case ADD_VISITED_NEWS :
                // buradan eklenecek
                return payload;            
        default: 
            return state;
    }
}